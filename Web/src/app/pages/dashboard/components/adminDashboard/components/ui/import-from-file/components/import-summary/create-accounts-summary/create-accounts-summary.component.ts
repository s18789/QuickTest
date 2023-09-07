import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CreatedAccountsSummary } from '../../../models/created-accounts-summary.model';
import { CreatedAccountsSummaryService } from '../../../services/created-accounts-summary.service'; 

@Component({
  selector: 'app-create-accounts-summary',
  templateUrl: './create-accounts-summary.component.html',
  styleUrls: ['./create-accounts-summary.component.css']
})
export class CreateAccountsSummaryComponent implements OnInit {

  public summary: CreatedAccountsSummary; 
  
  constructor(private router: Router, private summaryService: CreatedAccountsSummaryService
    ) { }

  ngOnInit(): void {
    this.summary = this.summaryService.getSummary();
  }

  goToDashboard(): void {
    this.router.navigate(['/dashboard']);
  }

}