package lol.schroeder.javametricsdemo

import org.springframework.http.ResponseEntity
import org.springframework.stereotype.Controller
import org.springframework.web.bind.annotation.GetMapping
import org.springframework.web.bind.annotation.RequestMapping
import java.util.*

@Controller
@RequestMapping("/foobar")
class FooBarController {

    @GetMapping
    fun fooBar(): ResponseEntity<Any> {
        val random = Random()
        val isError = random.nextInt(100) == 99

        if (isError) {
            Thread.sleep(random.nextInt(80).toLong() + 20)
            return ResponseEntity.status(500).build()
        }

        val isOutlier = random.nextInt(100) >= 94

        if (isOutlier) {
            Thread.sleep(random.nextInt(700).toLong() + 800)
        } else {
            Thread.sleep(random.nextInt(500).toLong() + 200)
        }

        return ResponseEntity.ok("OK")
    }

}