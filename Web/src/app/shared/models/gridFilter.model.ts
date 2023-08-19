import { SortEnum } from "../enums/sort.enum";

export interface GridFilter {
  search: string,
  sort: SortEnum,
  filter: any[],
}

export const defaultGridFilter: GridFilter = {
  search: "",
  sort: SortEnum.default,
  filter: [],
};


