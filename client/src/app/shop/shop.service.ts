import {Injectable} from "@angular/core";
import {HttpClient, HttpParams} from "@angular/common/http";
import {Pagination} from "../models/Pagination";
import {Product} from "../models/Product";
import {Brand} from "../models/Brand";
import {Type} from "../models/Type";
import {ShopParams} from "../models/ShopParams";

@Injectable(
  {providedIn:'root'}
)

export class ShopService{

  baseUrl = "https://localhost:5001/api/";



constructor(private http:HttpClient) {
}

getProduct(shopParams:ShopParams){


 let params = new HttpParams()

  if(shopParams.brandId > 0) params= params.append("BrandId",shopParams.brandId);
  if(shopParams.typeId> 0) params = params.append("TypeId",shopParams.typeId);
   params = params.append("sort",shopParams.sort)
  params = params.append('pageIndex',shopParams.pageIndex);
   params = params.append('pageSize',shopParams.pageSize);
    if(shopParams.search) params = params.append('search',shopParams.search)
  return this.http.get<Pagination<Product[]>>( this.baseUrl+"Products",{params:params});
}
  getBrands(){
    return this.http.get<Brand[]>( this.baseUrl+"Products/brands");
  }
  getTypes(){
    return this.http.get<Type[]>( this.baseUrl+"Products/types");
  }
}
