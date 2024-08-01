type Mods = Record<string, boolean | string>;

export function classNames(
  cls?: string,
  mods: Mods = {},
  additional: (string | undefined)[] = []
): string {
  return [
    ...additional.filter(Boolean),
    ...Object.entries(mods)
      .filter(([_, value]) => Boolean(value))
      .map(([className]) => className),
    cls ?? "",
  ].join(" ");
}

export type ClassNameProps<T, B extends boolean = true> = (B extends true
  ? { className?: string }
  : { styles?: string }) &
  T;
