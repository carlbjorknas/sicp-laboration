; Klassiskt rekursivt fakultet – SICP 1.2.1
(define (factorial n)
  (if (= n 1)
      1
      (* n (factorial (- n 1)))))

(factorial 5)
; --> 120
