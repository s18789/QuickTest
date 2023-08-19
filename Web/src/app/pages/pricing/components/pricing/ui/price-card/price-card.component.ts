import { Component, Input, OnInit } from '@angular/core';
import { PricingPlan } from 'src/app/pages/pricing/models/pricing.model';

@Component({
  selector: 'app-price-card',
  templateUrl: './price-card.component.html',
  styleUrls: ['./price-card.component.css']
})
export class PriceCardComponent implements OnInit {
  @Input() pricingPlan: PricingPlan;

  constructor() { }

  ngOnInit(): void {
  }

}
