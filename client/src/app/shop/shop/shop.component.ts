import {Component, ElementRef, OnInit, ViewChild} from '@angular/core';
import {ShopService} from "../shop.service";
import {Product} from "../../models/Product";
import {Brand} from "../../models/Brand";
import {Type} from "../../models/Type";
import {ShopParams} from "../../models/ShopParams";

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrl: './shop.component.scss'
})
export class ShopComponent implements OnInit {

  products: Product[] = [];
  brands: Brand[] = [];
  types: Type[] = [];
  shopParams = new ShopParams()
  @ViewChild('search') search?:ElementRef;
  totalCount = 0
  sortOptions = [
    {name:'Alphabetical',value:'name'},
    {name:'Highest price to Lowest price',value:'priceDesc'},
    {name:'Lowest price to Highest price',value: 'priceAsc'}

  ]

  constructor(private shopService: ShopService) {
  }

  ngOnInit(): void {
    this.getProducts();
    this.getBrands();
    this.getTypes();
  }

  getProducts() {
    this.shopService.getProducts(this.shopParams).subscribe({
      next: (response) => {
        this.products = response.data;
        this.shopParams.pageSize = response.pageSize;
        this.shopParams.pageIndex = response.pageIndex;
        this.totalCount = response.count
      },
      error: err => console.log(err)
    })
  }

  getBrands() {
    this.shopService.getBrands().subscribe({
      next: (response) => this.brands = [{id:0,name:'All'},...response],
      error: err => console.log(err)
    })
  }

  getTypes() {
    this.shopService.getTypes().subscribe({
      next: (response) => this.types = [{id:0,name:'All'},...response],
      error: err => console.log(err)
    })
  }

  onBrandSelected(brandId:number){
    this.shopParams.brandId = brandId;
    this.shopParams.pageIndex = 1;
    this.getProducts();
  }
  onTypeSelected(typeId:number){
    this.shopParams.typeId= typeId;
    this.shopParams.pageIndex = 1;
    this.getProducts();
  }
  onSortSelected(event:any){
    this.shopParams.sort = event.target.value;
    this.getProducts();
  }
  onPageChanged(event:any){
    if(this.shopParams.pageIndex !== event){
      this.shopParams.pageIndex = event;
      this.getProducts();
    }
  }
  onSearchClicked(){
    this.shopParams.search = this.search?.nativeElement.value;
    this.shopParams.pageIndex = 1;
    this.getProducts();
  }
  onResetClicked(){
    if(this.search)
    this.search.nativeElement.value = ''
    this.shopParams = new ShopParams()
    this.getProducts();
  }
}
