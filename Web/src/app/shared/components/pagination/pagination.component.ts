import { Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges } from '@angular/core';
import { Pagination } from '../../models/pagination.model';

@Component({
  selector: 'app-pagination',
  templateUrl: './pagination.component.html',
  styleUrls: ['./pagination.component.css']
})
export class PaginationComponent implements OnChanges {
  @Output() currentPageEvent = new EventEmitter<number>();

  @Input() data: Pagination;

  lastPage: number;

  constructor() {}

  ngOnChanges(changes: SimpleChanges) {
    this.lastPage = Math.ceil(this.data?.allItems / this.data?.itemsPerPage);
  }

  handlePageClick(value: number) {
    if (value == this.data.currentPage) {
      return;
    }

    this.currentPageEvent.emit(value);
  }

}
