package visidata.co.javaspringboot.controllers;

import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;
import visidata.co.javaspringboot.models.JsonWebTokens;

import java.util.HashMap;
import java.util.Map;

@RestController
public class TokenController {

    @CrossOrigin
    @RequestMapping(method = RequestMethod.GET, path = "/token")
    public ResponseEntity<Object> token(@RequestParam String username) {
        JsonWebTokens jwt = new JsonWebTokens(username);
        String token = jwt.getToken();

        Map<String, String> data = new HashMap<>();
        data.put("token", token);

        return new ResponseEntity<>(data, HttpStatus.OK);
    }
}
