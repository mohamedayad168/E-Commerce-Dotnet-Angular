import {Component, Input} from '@angular/core';

@Component({
  selector: 'app-paging-header',
  templateUrl: './paging-header.component.html',
  styleUrl: './paging-header.component.scss'
})
export class PagingHeaderComponent {
@Input() pageSize?:number;
@Input() pageIndex?:number;
@Input() count?:number;
}
