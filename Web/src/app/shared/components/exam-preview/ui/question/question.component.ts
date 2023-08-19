import { Component, Input, OnInit } from '@angular/core';
import { FormArray, FormGroup } from '@angular/forms';
import { QuestionType } from 'src/app/shared/enums/questionType.enum';
import { ExamPreviewType } from '../../enums/examPreviewType.enum';

@Component({
  selector: 'app-question',
  templateUrl: './question.component.html',
  styleUrls: ['./question.component.css']
})
export class QuestionComponent implements OnInit {
  @Input() question: FormGroup;
  @Input() previewType: ExamPreviewType;

  ExamPreviewType = ExamPreviewType;
  questionType = QuestionType;
  
  constructor() { }

  ngOnInit(): void {
  }

  get answers() {
    return this.question.controls['answers'] as FormArray<FormGroup>;
  }
}
