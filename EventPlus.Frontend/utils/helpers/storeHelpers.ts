import AsyncStorage from '@react-native-async-storage/async-storage';
import { create, StateCreator, StoreMutatorIdentifier } from "zustand";
import { createJSONStorage, devtools, persist, PersistOptions } from "zustand/middleware";
import { immer } from "zustand/middleware/immer";

export type ImmerStateCreator<
  StoreType,
  Mps extends [StoreMutatorIdentifier, unknown][] = [],
  Mcs extends [StoreMutatorIdentifier, unknown][] = []
> = StateCreator<StoreType, [...Mps, ["zustand/immer", never]], Mcs>;

export type SliceCreator<BoundType, SliceType> = (
  ...params: Parameters<ImmerStateCreator<BoundType>>
) => ReturnType<ImmerStateCreator<SliceType>>;

type PersistedStoreOptions<StoreType> = {
  persisted: boolean;
  name?: string;
} & Omit<PersistOptions<StoreType>, "name">;

const persistedStore = <StoreType>(
  name: string,
  options: Omit<PersistedStoreOptions<StoreType>, "name"> = {
    persisted: false,
  }
) => {
  if (options.persisted)
    return (value: any) =>
      persist(value, {
        ...options,
        name,
        storage: createJSONStorage(() => AsyncStorage)
      });

  return (value: any) => value;
};

export const createStore = <
  StoreType,
  Mos extends [StoreMutatorIdentifier, unknown][] = []
>(
  name: string,
  initializer: StateCreator<StoreType, [["zustand/immer", never]], Mos>,
  persistedOptions: PersistedStoreOptions<StoreType> = { persisted: false }
) => {
  const persistCustom = persistedStore(
    persistedOptions.name ?? name,
    persistedOptions
  );

  if (process.env.EXPO_PUBLIC_APP_MODE === "development") {
    return create<StoreType>()(
      devtools(persistCustom(immer(initializer)), { name })
    );
  }

  return create<StoreType>()(persistCustom(immer(initializer)));
};
