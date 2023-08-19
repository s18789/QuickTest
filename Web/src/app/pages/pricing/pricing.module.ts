import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { AngularSvgIconModule } from 'angular-svg-icon';
import { ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from 'src/app/shared/shared.module';
import { PricingComponent } from './components/pricing/pricing.component';
import { PriceCardComponent } from './components/pricing/ui/price-card/price-card.component';



@NgModule({
  declarations: [
  
    PricingComponent,
       PriceCardComponent
  ],
  imports: [
    CommonModule,
    FontAwesomeModule,
    ReactiveFormsModule,
    SharedModule,
    AngularSvgIconModule.forRoot(),
    RouterModule.forChild([
      {
        path: "",
        component: PricingComponent,
      },
    ]),
  ]
})

export class PricingModule { }
