import { AnswerDTO } from "./answerDTO";

export class QuestionDTO {
  QuestionTitle!: string;
  Points!: number;
  QuestionContent!: string;
  Answers!: AnswerDTO[];
}
