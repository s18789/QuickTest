import { HttpClient, HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { JwtModule } from '@auth0/angular-jwt';
import { FaIconLibrary, FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faClock } from '@fortawesome/free-regular-svg-icons';
import {
  faArrowLeftLong,
  faArrowRight,
  faArrowRightLong,
  faBars,
  faCertificate,
  faChevronDown,
  faChevronLeft,
  faChevronRight,
  faCircle,
  faCircleExclamation,
  faMagnifyingGlass,
  faPlus,
  faTrash,
  faXmark,
  faCircleXmark,
  faSort,
  faSortUp,
  faSortDown,
  faFileImport,
  faArrowUpLong,
  faArrowDownLong,
} from '@fortawesome/free-solid-svg-icons';
import { AngularSvgIconModule } from 'angular-svg-icon';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MaterialModule } from './material.module';
import { CoreModule } from './core/main/core.module';
import { SharedModule } from './shared/shared.module';

export function tokenGetter() {
  return localStorage.getItem("token");
}

@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    CoreModule,
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MaterialModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    FontAwesomeModule,
    SharedModule,
    AngularSvgIconModule.forRoot(),
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        allowedDomains: ["localhost:7067"],
        //disallowedRoutesRoutes: []
      }
    })
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {
  constructor(iconLibrary: FaIconLibrary) {
    iconLibrary.addIcons(
      faChevronLeft,
      faBars,
      faChevronDown,
      faPlus,
      faMagnifyingGlass,
      faChevronRight,
      faArrowLeftLong,
      faArrowRightLong,
      faClock,
      faXmark,
      faCircleExclamation,
      faArrowRight,
      faTrash,
      faCertificate,
      faCircle,
      faCircleXmark,
      faSort,
      faSortUp,
      faSortDown,
      faFileImport,
      faArrowUpLong,
      faArrowDownLong,);
  }
}
