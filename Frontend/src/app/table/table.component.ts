import { Observable } from 'rxjs';
import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';

import { GridComponent, GridDataResult, CancelEvent, EditEvent, RemoveEvent, SaveEvent, AddEvent } from '@progress/kendo-angular-grid';
import { State, process } from '@progress/kendo-data-query';
import { Product } from './Product';
import { EditService } from './edit.service';
import { map } from 'rxjs/operators';
import { AuthService } from '../_services/auth.service';
import { TokenStorageService } from '../_services/token-storage.service';

@Component({
    selector: 'table',
    template: `
   
        <kendo-grid
            [filterable]="true"
            [data]="view | async"
            [pageSize]="gridState.take"
            [skip]="gridState.skip"
            [sort]="gridState.sort"
            [pageable]="true"
            [sortable]="true"
            (dataStateChange)="onStateChange($event)"
            (edit)="editHandler($event)"
            (cancel)="cancelHandler($event)"
            (save)="saveHandler($event)"
            (remove)="removeHandler($event)"
            (add)="addHandler($event)"
            [navigable]="true"
            *ngIf="isLoggedIn"
        >
            <ng-template kendoGridToolbarTemplate>
                <button kendoGridAddCommand>Add new Product</button>
            </ng-template>
            <kendo-grid-column field="id"  title="Product Id"></kendo-grid-column>
            <kendo-grid-column  [filterable]="true" field="category" title="Category"></kendo-grid-column>
            <kendo-grid-column  [filterable]="true"field="pcode"  title="Product Code"></kendo-grid-column>
            <kendo-grid-column  [filterable]="true"field="pImage" title="Product Image"></kendo-grid-column>
            <kendo-grid-column  [filterable]="true"  field="price"  title="Price"></kendo-grid-column>
            <kendo-grid-column  [filterable]="true"  filter="numeric"  field="maximumQuantity"  title="Maximum Quantity"></kendo-grid-column>
            <kendo-grid-command-column title="Actions" [width]="220">
                <ng-template kendoGridCellTemplate let-isNew="isNew">
                    <button kendoGridEditCommand [primary]="true">Edit</button>
                    <button kendoGridRemoveCommand>Remove</button>
                    <button kendoGridSaveCommand [disabled]="formGroup!.invalid">{{ isNew ? 'Add' : 'Update' }}</button>
                    <button kendoGridCancelCommand>{{ isNew ? 'Discard changes' : 'Cancel' }}</button>
                </ng-template>
            </kendo-grid-command-column>
        </kendo-grid>
    `
})
export class TableComponent implements OnInit {
    public view!: Observable<GridDataResult>;
    public gridState: State = {
        sort: [],
        skip: 0,
        take: 5
    };
    public formGroup!: FormGroup;

    private editService: EditService;
    private editedRowIndex!: number;

    isLoggedIn = false;
    isLoginFailed = false;

    constructor(@Inject(EditService) editServiceFactory: () => EditService,private authService: AuthService, private tokenStorage: TokenStorageService) {
        this.editService = editServiceFactory();
    }

    public ngOnInit(): void {
        if (this.tokenStorage.getToken()) {
            this.isLoggedIn = true;
            //this.roles = this.tokenStorage.getUser().roles;
        this.view = this.editService.pipe(map((data) => process(data, this.gridState)));
     
        this.editService.read();
        }
    }

    public onStateChange(state: State): void {
        this.gridState = state;

        this.editService.read();
    }

    public addHandler(args: AddEvent): void {
        this.closeEditor(args.sender);
        this.formGroup = new FormGroup({
            id: new FormControl('',),
            category: new FormControl('', Validators.compose([Validators.required])),
            pcode    : new FormControl('',Validators.compose([Validators.required])),
            pImage: new FormControl('',Validators.compose([Validators.required])),
            price: new FormControl('',),
            maximumQuantity: new FormControl('',),
          
        });
        args.sender.addRow(this.formGroup);
    }

    public editHandler(args: EditEvent): void {
        
        const { dataItem } = args;  
        this.closeEditor(args.sender);

        this.formGroup = new FormGroup({
            id: new FormControl(dataItem.id),
            category: new FormControl(dataItem.category, Validators.compose([Validators.required])),
            pcode    : new FormControl(dataItem.pcode,Validators.compose([Validators.required])),
            pimage: new FormControl(dataItem.pImage,Validators.compose([Validators.required])),
            price: new FormControl(dataItem.price,),
            maximumquantity: new FormControl(dataItem. maximumQuantity,),

          
        });

        this.editedRowIndex = args.rowIndex;
        args.sender.editRow(args.rowIndex, this.formGroup);
    }

    public cancelHandler(args: CancelEvent): void {
        this.closeEditor(args.sender, args.rowIndex);
    }

    public saveHandler({sender, rowIndex, formGroup, isNew}: SaveEvent): void {
        const product: Product[] = formGroup.value;

        this.editService.save(product, isNew);

        sender.closeRow(rowIndex);
    }

    public removeHandler(args: RemoveEvent): void {
      
        this.editService.remove(args.dataItem);
    }

    private closeEditor(grid: GridComponent, rowIndex = this.editedRowIndex) {
        // close the editor
        grid.closeRow(rowIndex);
        // reset the helpers
        !this.editedRowIndex;
        !this.formGroup ;
    }
}