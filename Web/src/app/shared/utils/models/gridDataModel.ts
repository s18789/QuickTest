import { GridFieldTypes } from "./gridFieldTypes";

export interface GridDataModel<T> {
  key?: T;
  type?: GridFieldTypes;
  text?: string;
}
