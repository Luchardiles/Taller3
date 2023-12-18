import { Formik } from "formik";
import { View, StyleSheet } from "react-native";
import { Button, TextInput } from "react-native-paper";
import { SafeAreaView } from "react-native-safe-area-context";
import agent from "../../api/agent";
import { useRouter } from "expo-router";
import Toast from "react-native-root-toast";
import { format, clean } from "rut.js";

interface props {
  fullname: string;
  email: string;
  rut: string;
  birthday: string;
}

const RegisterScreen = () => {
  const router = useRouter();
  const handleSubmit = (data: props, resetForm: any) => {
    console.log(data);
    const birthday = parseInt(data.birthday);
    const rut = clean(data.rut);
    console.log(format(rut));
    if (
      data.fullname === "" ||
      data.email === "" ||
      data.rut === "" ||
      data.birthday === ""
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
    }
    agent.Auth.register(data.fullname, data.email, birthday, format(rut))
      .then((response) => {
        console.log(response);
        router.push("/");
        Toast.show("Registrado exitosamente", {
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
        resetForm();
      })
      .catch((error) => {
        console.log(error.data);
        console.log(error.data.status);
        let errorMessage: string = "Ocurrio un error. Intente denuevo.";
        switch (error.data.status) {
          case 400:
            if (error.data.errors?.Fullname) {
              if (
                error.data.errors.Fullname.includes(
                  "The fullname must be at least 10 characters"
                )
              ) {
                console.log(error.data.errors.Fullname);
                errorMessage = "El nombre debe tener al menos 10 caracteres.";
              } else if (
                error.data.errors.Fullname.includes(
                  "The fullname must be less than 150 characters"
                )
              ) {
                errorMessage = "El nombre debe tener menos de 150 caracteres.";
              }
            } else if (error.data.errors?.Rut) {
              if (error.data.errors.Rut.includes("The rut is not valid")) {
                errorMessage = "Rut inválido.";
              }
            } else if (error.data.errors?.Birthday) {
              if (
                error.data.errors.Birthday.includes("The birthday is not valid")
              ) {
                errorMessage = "Año de nacimiento inválido.";
              }
            } else if (error.data.errors?.Email) {
              if (error.data.errors.Email.includes("The email is not valid")) {
                errorMessage = "Correo electrónico inválido.";
              }
            }
            break;
          case 500:
            errorMessage = "Ocurrio un error. Intente denuevo.";
            break;
        }
        Toast.show(errorMessage, {
          duration: Toast.durations.LONG,
          position: Toast.positions.BOTTOM,
          shadow: true,
          animation: true,
          hideOnPress: true,
          delay: 0,
          containerStyle: {
            backgroundColor: "green",
          },
        });
      });
  };

  return (
    <SafeAreaView style={styles.container}>
      <Formik
        initialValues={{
          email: "",
          fullname: "",
          rut: "",
          birthday: "",
        }}
        onSubmit={(values, actions) => handleSubmit(values, actions.resetForm)}
      >
        {({ handleChange, handleBlur, handleSubmit, values }) => (
          <View style={styles.form}>
            <TextInput
              label="Nombre completo"
              value={values.fullname}
              onChangeText={handleChange("fullname")}
              onBlur={handleBlur("fullname")}
              style={styles.input}
              theme={{ colors: { primary: "black" } }}
            />
            <TextInput
              label="Año de nacimiento"
              value={values.birthday.toString()}
              onChangeText={handleChange("birthday")}
              onBlur={handleBlur("birthday")}
              style={styles.input}
              keyboardType="numeric"
              theme={{ colors: { primary: "black" } }}
              
            />
            <TextInput
              label="Rut"
              placeholder="Ej: 11.111.111-1"
              value={values.rut !== "" ? format(values.rut) : ""}
              onChangeText={handleChange("rut")}
              onBlur={handleBlur("rut")}
              style={styles.input}
              theme={{ colors: { primary: "black" } }}
            />
            <TextInput
              label="Correo electrónico"
              value={values.email}
              onChangeText={handleChange("email")}
              onBlur={handleBlur("email")}
              style={styles.input}
              theme={{ colors: { primary: "black" } }}
            />

            <Button
              mode="contained"
              onPress={() => handleSubmit()}
              style={styles.button}
            >
              Registrarme
            </Button>
          </View>
        )}
      </Formik>
    </SafeAreaView>
  );
};

export default RegisterScreen;

const styles = StyleSheet.create({
  container: {
    flex: 1,
    padding: 20,
    alignItems: "center",
    gap: 20,
    backgroundColor: "#fff",
  },
  button: {
    width: "100%",
    backgroundColor: "#0e22cf",
  },
  form: {
    width: "100%",
  },
  input: {
    marginBottom: 20,
    backgroundColor: "white",
    
  },
});
