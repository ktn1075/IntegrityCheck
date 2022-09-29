# Watchdog Project



## Concept

- Process 동작 감시 및 파일 무결성 검사 서비스 프로그램



## 기능 

- Target Process 동작 감시
  - 1초 마다 감시
    
- Target Process 최초 실행시 무결성 검사
  - 검사결과 log 기록
   
- Target Process 주기적 무결성 검사
  - 10분 마다 검사하며, 검사결과 log 기록

- Target Process 외부 요청에 의한 무결성 검사
  - Rest/Api 방식으로 요청이 들어오면 검사하며, 검사결과 log 기록 


## 기술스택 

- .NET Service
- .Rest/Api Server
