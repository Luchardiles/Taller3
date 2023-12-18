import { View, StyleSheet } from "react-native";
import { TextInput, Button } from "react-native-paper";
import { SafeAreaView } from "react-native-safe-area-context";
import agent from "../../api/agent";
import React from "react";
import { useRouter } from "expo-router";
import { useSelector } from "react-redux";
import { selectEmail } from "../../store/userSlice";
import Toast from "react-native-root-toast";
import { Formik } from "formik";
import { ChangePwd } from "../../models/ChangePwd";


const UpdateScreen = () => {
  const router = useRouter();
  const email = useSelector(selectEmail);

  const handleSubmit = (data: ChangePwd) => {
    if (
      data.currentPassword === "" ||
      data.newPassword === "" ||
      data.confirmNewPassword === ""
    ) {
      Toast.show("Algunos campos estan vacios", {
        duration: Toast.durations.LONG,
        position: Toast.positions.BOTTOM,
        shadow: true,
        animation: true,
        hideOnPress: true,
        delay: 0,
        containerStyle: {
          backgroundColor: "#FF0000",
        },
      });
      return;
    } else if (data.newPassword !== data.confirmNewPassword) {
      Toast.show("Las contraseñas no coinciden", {
        duration: Toast.durations.LONG,
        position: Toast.positions.BOTTOM,
        shadow: true,
        animation: true,
        hideOnPress: true,
        delay: 0,
        containerStyle: {
          backgroundColor: "#FF0000",
        },
      });
      return;
    }
    agent.Auth.password(email, data)
      .then((response) => {
        console.log(response);
        Toast.show("Contraseña cambiada", {
          duration: Toast.durations.LONG,
          position: Toast.positions.BOTTOM,
          shadow: true,
          animation: true,
          hideOnPress: true,
          delay: 0,
          containerStyle: {
            backgroundColor: "#4BB543",
          },
        });
        router.push("/(drawer)/repos/repository");
      })
      .catch((error) => {
        let errorDefault: string = "Ocurrio un error. Intente denuevo.";
        switch (error.status) {
          case 400:
            if (error.data == "Invalid Credentials") {
              errorDefault = "La contraseña actual es incorrecta.";
            }
            break;
          case 500:
            errorDefault = "Ocurrio un error. Intente denuevo.";
            break;
          default:
            break;
        }
        Toast.show(errorDefault, {
          duration: Toast.durations.LONG,
          position: Toast.positions.BOTTOM,
          shadow: true,
          animation: true,
          hideOnPress: true,
          delay: 0,
        });
      });
  };

  return (
    <SafeAreaView style={styles.container}>
      <Formik
        initialValues={{
          currentPassword: "",
          newPassword: "",
          confirmNewPassword: "",
        }}
        onSubmit={(values) => handleSubmit(values)}
      >
        {({ handleChange, handleBlur, handleSubmit, values }) => (
          <View style={styles.form}>
            <TextInput
              label="Contraseña actual"
              value={values.currentPassword}
              style={styles.input}
              onChangeText={handleChange("currentPassword")}
              onBlur={handleBlur("currentPassword")}
              secureTextEntry={true}
              theme={{ colors: { primary: "black" } }}
            />
            <TextInput
              label="Contraseña nueva"
              value={values.newPassword}
              style={styles.input}
              onChangeText={handleChange("newPassword")}
              onBlur={handleBlur("newPassword")}
              secureTextEntry={true}
              theme={{ colors: { primary: "black" } }}
            />
            <TextInput
              label="Confirmar contraseña nueva"
              value={values.confirmNewPassword}
              style={styles.input}
              onChangeText={handleChange("confirmNewPassword")}
              onBlur={handleBlur("confirmNewPassword")}
              secureTextEntry={true}
              theme={{ colors: { primary: "black" } }}
            />
            <Button
              mode="contained"
              onPress={() => handleSubmit()}
              style={styles.button}
            >
              Cambiar contraseña
            </Button>
          </View>
        )}
      </Formik>
    </SafeAreaView>
  );
};

export default UpdateScreen;

const styles = StyleSheet.create({
  container: {
    flex: 1,
    padding: 20,
    alignItems: "center",
    gap: 20,
  },
  button: {
    width: "100%",
    backgroundColor: "#0e22cf"
  },
  form: {
    width: "100%",
    padding: 20,
    alignItems: "center",
    gap: 20,
    flex: 1,
  },
  input: {
    width: "100%",
    backgroundColor: "white",
  },
});
