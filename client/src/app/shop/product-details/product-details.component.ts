import {Component, OnInit} from '@angular/core';
import {Product} from "../../models/Product";
import {ShopService} from "../shop.service";
import {ActivatedRoute} from "@angular/router";

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrl: './product-details.component.scss'
})
export class ProductDetailsComponent implements OnInit{
  constructor(private shopService:ShopService,private activeRoute:ActivatedRoute) {
  }
product?:Product;
getProduct(){
  const id = this.activeRoute.snapshot.paramMap.get('id')
  if(id)
  {
    this.shopService.getProduct(+id).subscribe({
      next: product => this.product = product,
      error: err => console.log(err)
    })
  }
}

  ngOnInit(): void {
  this.getProduct()
  }
}
