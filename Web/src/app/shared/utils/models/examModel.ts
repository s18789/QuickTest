import { AnswerDTO } from "./answerDTO";
import { QuestionDTO } from "./questionDTO";
import { StudentDTO } from "./studentDTO";

interface formExam {
  title: string,
  time: number,
  availableFrom: Date,
  availableTo: Date,
  questions: {
    questionTitle: string,
      points: number,
      questionContent: string,
      answers: {
        answerContent: string,
        correct: boolean,
      }[]
  }[],
  students: {
    index: string
  }[]
}


export class ExamDTO {
  Title!: string;
  Time!: number;
  AvailableFrom!: Date;
  AvailableTo!: Date;
  Questions!: QuestionDTO[];
  Students!: StudentDTO[];

  constructor(examForm: formExam){
    this.Title = examForm.title,
    this.Time = examForm.time,
    this.AvailableFrom = examForm.availableFrom,
    this.AvailableTo = examForm.availableTo,
    this.Questions = examForm.questions.map(q => {
      return {
        QuestionTitle: q.questionTitle,
        Points: q.points,
        QuestionContent: q.questionContent,
        Answers: q.answers.map(a => {
          return {
            AnswerContent: a.answerContent,
            Correct: a.correct
          } as AnswerDTO
        })
      } as QuestionDTO
    }),
    this.Students = examForm.students.map(s => {
      return {
        Index: s.index
      } as StudentDTO
    })
  }
}
