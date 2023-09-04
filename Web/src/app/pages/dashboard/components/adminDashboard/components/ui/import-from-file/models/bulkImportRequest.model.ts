import { ImportSummaryDto } from './import-summary.model'; // Import your ImportSummaryDto model here

export interface BulkImportRequest {
  ImportSummary: ImportSummaryDto;
  SchoolId: number;
}