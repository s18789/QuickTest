import { Component } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent {
  title = 'QuickTest';

  constructor(
    public translate: TranslateService
  ){
    translate.addLangs(['en', 'ja']);
    translate.setDefaultLang('en');
  } 
}
