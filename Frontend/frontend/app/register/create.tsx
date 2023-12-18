import RegisterScreen from "../../components/register/RegisterScreen";
import { Stack } from "expo-router";
const Register = () => {
  return (
    <>
      <Stack.Screen
        options={{
          title: "Registrar",
          headerShown: true,
          headerBackButtonMenuEnabled: true,
        }}
      />
      <RegisterScreen />
    </>
  );
};

export default Register;
