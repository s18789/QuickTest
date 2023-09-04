import { Injectable } from "@angular/core";

@Injectable({
    providedIn: 'root'
  })
  export class ImportIdService {
    private importId: string;
  
    setImportId(id: string): void {
      this.importId = id;
    }
  
    getImportId(): string {
      return this.importId;
    }
  }