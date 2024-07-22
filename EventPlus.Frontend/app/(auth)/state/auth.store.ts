import { FirebaseAuthTypes } from "@react-native-firebase/auth";
import { createStore } from "~/utils/helpers/storeHelpers";
import { AuthStoreSchema } from "./auth.schema";

export const useAuthStore = createStore<AuthStoreSchema>(
  "Auth Store",
  (set) => {
    const setProviderUser = (user: FirebaseAuthTypes.User) => {
      set((state) => {
        state.providerUser = user;
      });
    };
    return {
      providerUser: null,
      setProviderUser,
    };
  }
);
