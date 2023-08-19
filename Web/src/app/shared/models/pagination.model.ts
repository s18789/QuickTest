export interface Pagination {
  itemsPerPage: number;
  allItems: number;
  currentPage: number;
}

export const defaultPagination: Pagination = {
  allItems: 0,
  currentPage: 1,
  itemsPerPage: 9,
};
