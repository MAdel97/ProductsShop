import { Injectable } from '@angular/core';
import { Observable, BehaviorSubject, observable } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import {  Product } from './Product';
import { tap, map } from 'rxjs/operators';

const CREATE_ACTION = 'AddProduct';
const UPDATE_ACTION = 'UpdateProduct';

@Injectable()
export class EditService extends BehaviorSubject<Product[]> {
    constructor(private http: HttpClient) {
        super([]);
    }

    private data: Product[]= [];

    public read(): void {
        if (this.data.length) {
            return super.next(this.data);
        }

        this.fetch()
            .pipe(
                tap(data => {
                    this.data = data;
                })
            )
            .subscribe(data => {
                super.next(data);
            });
    }

    public save(data: Product[], isNew?: boolean): void {
        const action = isNew ? CREATE_ACTION : UPDATE_ACTION;

        this.reset();

        this.update( data,action)
            .subscribe(() => this.read(), () => this.read());
    }

    public remove(data: Product []): void {
        this.reset();

        this.delete(data)
            .subscribe(() => this.read(), () => this.read());
    }

    public resetItem(dataItem: Product): void {
        if (!dataItem) { return; }

        // find orignal data item
        const originalDataItem = this.data.find(item => item.id === dataItem.id);

        // revert changes
        Object.assign(!originalDataItem, dataItem);

        super.next(this.data);
    }

    private reset() {
        this.data = [];
    }

    private fetch(action = "", data?: Product[]): Observable<Product[]> {
        var resp= this.http
          .get<Product[]>(
                `https://localhost:44353/products/getproducts`)

          .pipe(map((res: Object) => <Product[]>res));
          console.log(resp);
          return resp;
      }
    

    private update(data?:Product[],action=""):Observable<Product[]> {
       
        let body = JSON.stringify(data);
        console.log(body);
         let headers = new HttpHeaders({'Content-Type': 'application/json'});
        const  options ={
                headers:headers
      }
      const res=  this.http.post(`https://localhost:44353/products/${action}`,body   ,options)  
        .pipe(map((res: Object) => <Product[]>res));
        return res;
    }


    private delete(data?:Product[]):Observable<Product[]> {
        
        console.log(data)
        let body = JSON.stringify(data);
         let headers = new HttpHeaders({'Content-Type': 'application/json'});
        const  options ={
                headers:headers
      }
    
   
    const res=  this.http.post(`https://localhost:44353/products/DeleteProduct`,body   ,options)  
    .pipe(map((res: Object) => <Product[]>res));
        return res;
    }
   
}
