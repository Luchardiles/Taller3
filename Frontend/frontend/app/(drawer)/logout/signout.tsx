import { useDispatch } from "react-redux";
import { logout } from "../../../store/userSlice";
import { useRouter } from "expo-router";
import { useEffect } from "react";
import AsyncStorage from "@react-native-async-storage/async-storage";

const SignOff = () => {
  const dispatch = useDispatch();
  const route = useRouter();

  useEffect(() => {
    dispatch(logout());
    AsyncStorage.removeItem("token");

    route.replace("/");
  }, []);

  return null;
};

export default SignOff;
