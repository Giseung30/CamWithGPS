# Camera and GPS
> GPS와 카메라를 함께 사용해보자

## 📣 프로젝트 목적
+ AR을 위해 다른 API를 사용하지 않고 직접 개발해보고자 테스트용으로 작성했다.
+ 범용적으로 사용할 수 있는 GPS와 Camera 기능을 만들어본다.

## ⚙ 개발 환경
+ `Unity 2022.3.5f1`
+ `Visual Studio Code`

## ⏲ 개발 기간
+ 2023.08.06 ~ 2023.08.16
  
## 📌 개발 내용
+ 핵심 스크립트는 `GPSModule.cs`, `WebCamModule.cs`이다.
    + GPS 모듈은 ***GetGPSLocation*** 함수로 위도와 경도 값을 받아올 수 있다.
    + 카메라는 Canvas 상의 ***RawImage*** 컴포넌트를 통하여 나타난다.
+ 이 모듈들은 권한 요청이 필요하므로, 권한을 통괄하는 스크립트인 `PermissionModule.cs`를 작성했다.
    + 요청하고자 하는 권한을 따로 설정할 수 있고, 허용된 권한은 콜백 함수의 파라미터로 구분할 수 있다.
+ 권한 획득 후, 실행되는 콜백 함수를 연동하기 위해 `MainManager.cs` 스크립트를 작성했다.
    + GPS의 경도, 위도 값을 표시하기 위한 코드도 추가되어있다.

<div align="center">
  <img width="75%" height="75%" src="https://github.com/Giseung30/Camera_and_GPS/assets/60832219/4b1de652-dd99-46a7-8950-fd20b56137e3"/>
</div>

## 🎬 미리보기
<div align="center">
  <img width="30%" height="30%" src="https://github.com/Giseung30/Camera_and_GPS/assets/60832219/fc69b8c8-2efc-4991-9d9b-629b15fe29fe"/>
</div>

## 🔗 링크
+ [Unity] 안드로이드(Android) 카메라 연동하기 : **https://giseung.tistory.com/42**
+ [Unity] GPS 기능으로 위도와 경도 불러오기 : **https://giseung.tistory.com/43**