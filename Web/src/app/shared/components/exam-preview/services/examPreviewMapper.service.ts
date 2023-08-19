import { Injectable } from "@angular/core";
import { ExamPreviewResponse } from "../models/examPreviewResponse.model";
import { AnswerPreview, ExamPreview, ExamPreviewForm, QuestionPreview } from "../models/examPreview.model";
import { FormArray, FormBuilder, FormControl, FormGroup } from "@angular/forms";
import { QuestionType } from "src/app/shared/enums/questionType.enum";

@Injectable({
  providedIn: "root",
})

export class ExamPreviewMapperService {
  constructor(
    private formBuilder: FormBuilder,
  ) { }

  mapExamPreviewResponseToExamPreview(examPreviewResponse: ExamPreviewResponse): ExamPreview {
    return {
      ...examPreviewResponse,
      examResultId: null,
      questions: examPreviewResponse.questions.map(q => {
        var question: QuestionPreview = {
          ...q,
          score: null,
          answerContent: q.answerContent,
          answers: q.answers
        }

        return question;
      })
    };
  }

  mpaExamPreviewToExamPreviewForm(examPreview: ExamPreview): FormGroup<ExamPreviewForm> {
    const examFormGroup = new FormGroup<ExamPreviewForm>({
      title: new FormControl(examPreview.title),
      questions: new FormArray<FormGroup>(([]))
    });
    const questionformArray = examFormGroup.controls['questions'] as FormArray<FormGroup>;

    examPreview.questions.forEach((q, i) => {
      questionformArray.push(this.formBuilder.group({
        questionId: q.questionId,
        content: q.content,
        type:  q.type,
        answerContent: q.type == QuestionType.Open ? q.answerContent : null,
        answers: q.type == QuestionType.Open ? null : new FormArray<FormGroup>([])
      }));

      const answerFormArray = questionformArray.at(i)?.controls['answers'] as FormArray<FormGroup>;

      if (q.type != QuestionType.Open) {
        q.answers.forEach((a) => {
          answerFormArray.push(this.formBuilder.group({
            answerId: a.answerId,
            content: a.content,
            isCorrect: a.isCorrect,
            isSelected: a.isSelected
          }))
        })
        
      }
    });

    return examFormGroup;
  }
}