import { FirebaseAuthTypes } from "@react-native-firebase/auth";

interface StoreData {
    providerUser: FirebaseAuthTypes.User | null
}

interface StoreActions {
    setProviderUser: (user: FirebaseAuthTypes.User) => void
}

export type AuthStoreSchema = StoreData & StoreActions;
