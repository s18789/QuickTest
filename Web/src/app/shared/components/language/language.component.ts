import { Component } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-language',
  templateUrl: './language.component.html',
  styleUrls: ['./language.component.css']
})
export class LanguageComponent {
  constructor(
    private translate: TranslateService
  ) { }

  changeLanguage(language: string) {
    this.translate.use(language);
  }
}
