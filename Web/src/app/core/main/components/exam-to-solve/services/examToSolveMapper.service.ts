import { Injectable } from "@angular/core";
import { ExamToSolveResponse } from "../models/examToSolveResponse";
import { Answer, ExamToSolve, ExamToSolveForm, Question, QuestionDTO, ResolvedExamDTO } from "../models/examToSolve.model";
import { FormArray, FormBuilder, FormControl, FormGroup } from "@angular/forms";
import { QuestionType } from "src/app/shared/enums/questionType.enum";

@Injectable({
  providedIn: "root",
})

export class ExamToSolveMapperService {
  constructor(
    private formBuilder: FormBuilder,
  ) { }

  mapExamToSolveResponseToExamToSolve(examToSolveResponse: ExamToSolveResponse): ExamToSolve {
    return {
      title: examToSolveResponse.title,
      questions: examToSolveResponse.questions.map(q => {
        var question: Question = {
            questionId: q.questionId,
            type: !q.type ? QuestionType.MultipleChoice : q.type,
            content: q.content,
            answerContent: null,
            answers: q.type == QuestionType.Open
                ? null
                : q.answers.map(a => {
                    var answer: Answer = {
                        answerId: a.answerId,
                        content: a.content,
                        isSelected: false,
                    }
                    return answer;
                })
        }
        return question;
      })
    };
  }

  mapExamToSolveToExamToSolveFormGroup(examToSolve: ExamToSolve): FormGroup<ExamToSolveForm> {
    const examFormGroup = new FormGroup<ExamToSolveForm>({
      title: new FormControl(examToSolve.title), 
      questions: new FormArray<FormGroup>(([]))
    });
    const questionformArray = examFormGroup.controls['questions'] as FormArray<FormGroup>;

    examToSolve.questions.forEach((q, i) => {
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
            isSelected: null
          }))
        })
        
      }
    });

    return examFormGroup;
  }

  mapExamToSolveToResolvedExam(examResultId: string, examToSolve: ExamToSolve): ResolvedExamDTO {
    return {
      examResultId: examResultId,
      questions: examToSolve.questions.map(q => {
        var question: QuestionDTO = {
          questionId: q.questionId,
          type: !q.type  ? Number(QuestionType.MultipleChoice) : Number(q.type),
          content: q.content,
          answerContent: q.answerContent,
          answers: q.answers.map(a => {
            var answer: Answer = {
              answerId: a.answerId,
              content: a.content,
              isSelected: !a.isSelected ? false : true
            }

            return answer;
          })
        }

        return question;
      })
    }
  }

}
