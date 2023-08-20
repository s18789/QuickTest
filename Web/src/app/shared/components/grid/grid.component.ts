import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Pagination, defaultPagination } from '../../models/pagination.model';
import { Subject, tap } from 'rxjs';
import { SortEnum } from '../../enums/sort.enum';
import { GridFilter, defaultGridFilter } from '../../models/gridFilter.model';
import { ConfigurationItemType } from '../../utils/model/enums/configurationItemType.enum';
import { ActionConfiguration } from '../../utils/model/actionConfiguration.model';
import { GridItemConfiguration } from '../../utils/model/GridConfiguration.model';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmationDialogComponent } from '../confirmation-dialog/confirmation-dialog.component';
import { ConfirmationDialogType } from '../../enums/confirmationDialogType.enum';
import { ConfirmationDialogActionType } from '../../enums/confirmationDialogActionType.enum';
import { ExamSolveService } from 'src/app/core/main/services/examSolveService.service';
import { ExamResultStatus } from 'src/app/pages/exams-results/enums/examResultStatus.enum';

@Component({
  selector: 'app-grid',
  templateUrl: './grid.component.html',
  styleUrls: ['./grid.component.css']
})

export class GridComponent {

  @Input() routerName: string;
  @Input() configurations: GridItemConfiguration[];
  @Input() allItems: any[] | null;
  @Input() searchPlaceholder: string;
  @Input() searchConfiguration: ActionConfiguration;
  @Input() filterConfiguration: ActionConfiguration;
  @Output() action: EventEmitter<any> = new EventEmitter();

  itemView$ = new Subject<any[]>();
  sort: typeof SortEnum = SortEnum;
  showFilterModal = false;
  configurationItemType = ConfigurationItemType;
  startExamDialogCount: number = 0;

  pagination: Pagination = {
    ...defaultPagination,
  };
  itemFilters: GridFilter = {
    ...defaultGridFilter,
  };

  constructor(
    private router: Router,
    private dialog: MatDialog,
    private examSolveService: ExamSolveService,
    ) { }

  searchValueChange() {
    this.pagination.currentPage = 1;
    this.refreshItemsView();
  }

  clearSearch() {
    this.itemFilters.search = '';
    this.searchValueChange();
  }

  getAllFilters() {
    return new Set(this.allItems.map(item => this.getPropertyBasedOnType(item, this.filterConfiguration)));
  }

  onCurrentPageChange(currentPage: number) {
    this.pagination.currentPage = currentPage;
    this.refreshItemsView();
  }

  handleSortChange() {
    this.itemFilters.sort = this.itemFilters.sort == SortEnum.asc
      ? SortEnum.desc
      : SortEnum.asc;

    this.refreshItemsView();
  }

  handleFilterClick() {
    this.showFilterModal = !this.showFilterModal;
  }

  handleFilterChange(filterItem: any) {
    if (this.itemFilters.filter.includes(filterItem)) {
      this.itemFilters.filter.splice(
        this.itemFilters.filter.indexOf(filterItem),
        1
      );
    } else {
      this.itemFilters.filter.push(filterItem);
    }

    this.pagination.currentPage = 1;
    this.refreshItemsView();
  }

  private refreshItemsView() {
    //search filter
    const filteredItems = this.allItems?.filter(
      (item) =>
        !this.itemFilters.filter.includes(this.getPropertyBasedOnType(item, this.filterConfiguration)) &&
        this.getPropertyBasedOnType(item, this.searchConfiguration)
          .toLocaleLowerCase()
          .includes(this.itemFilters.search.toLocaleLowerCase())
    );

    //sort
    let sortedItems = filteredItems;
    switch (this.itemFilters.sort) {
      case SortEnum.asc:
        sortedItems = sortedItems?.sort((a, b) =>
        this.getPropertyBasedOnType(a, this.searchConfiguration)
            .toLocaleLowerCase()
            .localeCompare(this.getPropertyBasedOnType(b, this.searchConfiguration).toLocaleLowerCase())
        );
        break;

      case SortEnum.desc:
        sortedItems = sortedItems?.sort((a, b) =>
        this.getPropertyBasedOnType(b, this.searchConfiguration)
            .toLocaleLowerCase()
            .localeCompare(this.getPropertyBasedOnType(a, this.searchConfiguration).toLocaleLowerCase())
        );

        break;
    }

    //pagination
    this.pagination = { ...this.pagination, allItems: sortedItems?.length };
    const from: number =
      (this.pagination.currentPage - 1) * this.pagination.itemsPerPage;
    const to: number =
      this.pagination.currentPage * this.pagination.itemsPerPage;
    const paginatedItems = sortedItems?.slice(from, to);

    this.itemView$.next(paginatedItems);
  }

  getPropertyBasedOnType(item: any, property: ActionConfiguration): string {
    return property?.type == ConfigurationItemType.object
      ? item[property.propertyName][property.nestedPropertyName]
      : item[property.propertyName];
  }

  handleAction(item: any) {
    if (this.routerName.includes("examResult")) {
      switch(item.status) {
        case ExamResultStatus.NotResolved:
          this.openExamStartConfirmationDialog(item.id);
        break;
        case ExamResultStatus.ToCheck:
          this.router.navigate([`${this.routerName}/${item.id}/check`]);
        break;
        default:
          this.router.navigate([`${this.routerName}/${item.id}`]);
      }
      return;
    }

    this.router.navigate([`${this.routerName}/${item.id}`]);
  }

  openExamStartConfirmationDialog(id: string) {
      const dialogRef = this.dialog.open(ConfirmationDialogComponent, {
        data: {
          title: "Start exam",
          description: "Are you sure you want to start the exam?",
          dialogType: ConfirmationDialogType.Confimation,
          cancelButtonText: "Cancel",
          confirmButtonText: "Start",
          dialogCount: this.startExamDialogCount,
        },
      }, );
  
      dialogRef.afterClosed().pipe(
        tap((actionType: ConfirmationDialogActionType) => {
          if (actionType == ConfirmationDialogActionType.Submit) {
            this.examSolveService.prepareExam(id);
          }
  
          this.startExamDialogCount++;
        })
      ).subscribe();
    }
}
