package visidata.co.javaspringboot.controllers;

import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
public class HelloWorldController {
    @RequestMapping("/")
    public String helloWorld() {
        return "Hello, World!";
    }

    @RequestMapping("/goodbye")
    public String goodBye() {
        return "Goodbye!";
    }
}
