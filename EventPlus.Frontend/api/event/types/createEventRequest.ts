import { DateType } from "react-native-ui-datepicker";
import { Priority } from "~/api/types/Priority";

export interface CreateEventRequest {
  title: string;
  description?: string;
  priority: Priority;
  date: DateType;
}
