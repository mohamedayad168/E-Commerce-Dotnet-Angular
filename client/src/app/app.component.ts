import {Component, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Product} from "./models/Product";
import {Pagination} from "./models/Pagination";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent implements OnInit{
  products: Product[] = []
  title:string = "App"
  constructor(private http:HttpClient) {
  }
  ngOnInit(): void {
    this.http.get<Pagination<Product>[]>("https://localhost:5001/api/Products?PageSize=50").subscribe({
      next: (response:any) => this.products = response.data,
      error: err => console.log(err),
      complete: () => {
        console.log("completed")
      }
    })
  }

}
