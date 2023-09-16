import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CreatedAccountsSummary } from '../../../models/created-accounts-summary.model';
import { CreatedAccountsSummaryService } from '../../../services/created-accounts-summary.service'; 
import { ErrorHandler } from '@angular/core';

@Component({
  selector: 'app-create-accounts-summary',
  templateUrl: './create-accounts-summary.component.html',
  styleUrls: ['./create-accounts-summary.component.css']
})
export class CreateAccountsSummaryComponent implements OnInit , ErrorHandler{

  public summary: CreatedAccountsSummary; 
  
  constructor(private router: Router, private summaryService: CreatedAccountsSummaryService
    ) { }

  ngOnInit(): void {
    this.summary = this.summaryService.getSummary();
    console.log('create-accounts-summary component initialized');
  }
  handleError(error: any): void {
    debugger;
    console.error('An error occurred:', error);
  }

  goToDashboard(): void {
    this.router.navigate(['/dashboard']);
  }

}