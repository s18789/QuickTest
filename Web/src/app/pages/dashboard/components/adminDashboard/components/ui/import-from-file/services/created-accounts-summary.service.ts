import { Injectable } from '@angular/core';
import { CreatedAccountsSummary } from '../models/created-accounts-summary.model';

@Injectable({
  providedIn: 'root'
})
export class CreatedAccountsSummaryService {

  private summary: CreatedAccountsSummary;

  constructor() { }

  setSummary(summary: CreatedAccountsSummary): void {
    this.summary = summary;
    console.log('Summary set:', this.summary);
  }

  getSummary(): CreatedAccountsSummary {
    return this.summary;
  }
}