import { Observable } from "rxjs";

export interface HttpServiceModel {
  fetch(filters?: { [key: string]: any }): Observable<any>;
  get(id: string): Observable<any>;
  add(item: any): Observable<any>;
  update(item: any): Observable<any>;
  remove(id: string): Observable<any>;
}
