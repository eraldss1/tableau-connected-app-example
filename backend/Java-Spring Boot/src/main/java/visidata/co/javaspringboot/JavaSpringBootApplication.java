package visidata.co.javaspringboot;

import io.github.cdimascio.dotenv.Dotenv;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;

import java.util.Collections;


@SpringBootApplication
public class JavaSpringBootApplication {

    public static void main(String[] args) {
        Dotenv dotenv = Dotenv.load();

        SpringApplication app = new SpringApplication(JavaSpringBootApplication.class);
        app.setDefaultProperties(
                Collections.singletonMap(
                        "server.port", dotenv.get("PORT")
                )
        );

        app.run(args);
    }

}
