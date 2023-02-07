const protocol = window.location.protocol.replace("http", "ws");
const host = window.location.host;
const gameUrl = `${protocol}//${host}/play`;

export class Api {
  private static DATA_BASE_END_POINT = "https://api.debugger.pl/";
  private static AUTH_BASE_END_POINT = "https://auth.debugger.pl/";

  private static LOCAL_BASE_END_POINT = "https://localhost:7067/api/";

  // Data end-points
  static DATA_ITEMS = Api.DATA_BASE_END_POINT + "items";
  static DATA_UPLOAD = Api.DATA_BASE_END_POINT + "utils/upload";
  static DATA_ITEM_EXISTS = Api.DATA_BASE_END_POINT + "utils/exists";
  static DATA_WORKERS = Api.DATA_BASE_END_POINT + "workers";
  static DATA_FORM_CONFIG =
    Api.DATA_BASE_END_POINT + "utils/register-config-form";

  // Auth end-points
  static AUTH_LOGIN = Api.AUTH_BASE_END_POINT + "login";
  static AUTH_LOGOUT = Api.AUTH_BASE_END_POINT + "logout";
  static AUTH_IS_LOGGED = Api.AUTH_BASE_END_POINT + "is-logged";
  static AUTH_REFRESH_TOKEN = Api.AUTH_BASE_END_POINT + "refresh-token";
  static AUTH_REGISTER = Api.AUTH_BASE_END_POINT + "register";
  static AUTH_UNREGISTER = Api.AUTH_BASE_END_POINT + "unregister";
  static AUTH_CHECK_USERNAME = Api.AUTH_BASE_END_POINT + "exists";
  static AUTH_PROFILE = Api.AUTH_BASE_END_POINT + "data";

  // WebSocket Game end-points
  static GAME_PLAY = gameUrl;
  static GAME_GET_USER = "/get-user";
  static GAME_REGISTER_USER = "/register-user";

  // Local
  static DATA_EXAMS = Api.LOCAL_BASE_END_POINT + "exams";
  static DATA_STUDENTS = Api.LOCAL_BASE_END_POINT + "students";

  static LOCAL_URL = Api.LOCAL_BASE_END_POINT;
}
