export type FormErrors<T> = {
  [Key in keyof T]?: string;
};
