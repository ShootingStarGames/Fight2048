using UnityEngine;

using System;

using System.Collections;

using GooglePlayGames;

using GooglePlayGames.BasicApi;

using GooglePlayGames.BasicApi.SavedGame;

using UnityEngine.SocialPlatforms;

public static class CGoogleplayGameServiceManager

{
    static byte[] bytes;
    //게임서비스 플러그인 초기화시에 EnableSavedGames()를 넣어서 저장된 게임 사용할 수 있게 합니다.

    //주의 하실점은 구글플레이 개발자 콘솔의 게임서비스에서 해당게임의 세부정보에서 저장된 게임 사용을 

    //하도록 설정하셔야 합니다.

    public static string GetNameGPGS()
    {
        if (Social.localUser.authenticated)
            return Social.localUser.userName;
        else
            return null;
    }

    public static void SetSaveData()
    {
        
    }

    public static void Init()

    {
#if UNITY_ANDROID
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().EnableSavedGames().Build();


        PlayGamesPlatform.InitializeInstance(config);


        //

        PlayGamesPlatform.DebugLogEnabled = false;

        PlayGamesPlatform.Activate();

        //Activate the Google Play gaems platform

#endif


   
    }

    public static void Login()
    {
        if (!CheckLogin())

        {

            Social.localUser.Authenticate((bool success) => {
                //handle success or failure
            });

        }
    }

    public static void SumbitScore(int score)
    {
        Social.ReportScore(score, "CgkIoKuR2IoYEAIQAQ", (bool success) => {
            if (success)
            {
                Debug.Log("Success");
            }
        });

    }

    public static void Achivement()
    {
        // unlock achievement (achievement ID "Cfjewijawiu_QA")
        Social.ReportProgress("CgkIg9Lj0q8bEAIQBg", 100.0f, (bool success) => {
            //handle success or failure
        });
        //인증여부 확인
    }

    public static void ShowLeaderboard()
    {
        Social.ShowLeaderboardUI();
    }
    public static void ShowAchivement()
    {
        Social.ShowAchievementsUI();
    }

    public static bool CheckLogin()

    {

        return Social.localUser.authenticated;

    }

    //--------------------------------------------------------------------

    //게임 저장은 다음과 같이 합니다.

    //public static void SaveToCloud(string key)

    //{

    //    if (!CheckLogin()) //로그인되지 않았으면

    //    {

    //        //로그인루틴을 진행하던지 합니다.
    //        Login();
    //        return;

    //    }
    //    bytes=System.Text.Encoding.UTF8.GetBytes(FileData.DatatoString());
    //    OpenSavedGame(key, true);

    //}


    //static void OpenSavedGame(string filename, bool bSave)

    //{

    //    ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;

    //    if (bSave)

    //        savedGameClient.OpenWithAutomaticConflictResolution(filename, DataSource.ReadCacheOrNetwork, ConflictResolutionStrategy.UseLongestPlaytime, OnSavedGameOpenedToSave); //저장루틴진행

    //    else

    //        savedGameClient.OpenWithAutomaticConflictResolution(filename, DataSource.ReadCacheOrNetwork, ConflictResolutionStrategy.UseLongestPlaytime, OnSavedGameOpenedToRead); //로딩루틴 진행

    //}



    ////savedGameClient.OpenWithAutomaticConflictResolution호출시 아래 함수를 콜백으로 지정했습니다. 준비된경우 자동으로 호출될겁니다.

    //static void OnSavedGameOpenedToSave(SavedGameRequestStatus status, ISavedGameMetadata game)

    //{

    //    if (status == SavedGameRequestStatus.Success)

    //    {

    //        // handle reading or writing of saved game.


    //        //파일이 준비되었습니다. 실제 게임 저장을 수행합니다.

    //        //저장할데이터바이트배열에 저장하실 데이터의 바이트 배열을 지정합니다.

    //        SaveGame(game,bytes, DateTime.Now.TimeOfDay);

    //    }

    //    else

    //    {

    //        //파일열기에 실패 했습니다. 오류메시지를 출력하든지 합니다.

    //    }

    //}


    //static void SaveGame(ISavedGameMetadata game, byte[] savedData, TimeSpan totalPlaytime)

    //{

    //    ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;


    //    SavedGameMetadataUpdate.Builder builder = new SavedGameMetadataUpdate.Builder();

    //    builder = builder

    //        .WithUpdatedPlayedTime(totalPlaytime)

    //        .WithUpdatedDescription("Saved game at " + DateTime.Now);


    //    /*

    //    if (savedImage != null)

    //    {

    //        // This assumes that savedImage is an instance of Texture2D

    //        // and that you have already called a function equivalent to

    //        // getScreenshot() to set savedImage

    //        // NOTE: see sample definition of getScreenshot() method below

    //        byte[] pngData = savedImage.EncodeToPNG();

    //        builder = builder.WithUpdatedPngCoverImage(pngData);

    //    }*/


    //    SavedGameMetadataUpdate updatedMetadata = builder.Build();

    //    savedGameClient.CommitUpdate(game, updatedMetadata, savedData, OnSavedGameWritten);

    //}

    //static void OnSavedGameWritten(SavedGameRequestStatus status, ISavedGameMetadata game)

    //{




    //    if (status == SavedGameRequestStatus.Success)

    //    {

    //        //데이터 저장이 완료되었습니다.

    //    }

    //    else

    //    {

    //        //데이터 저장에 실패 했습니다.

    //    }

    //}


    ////----------------------------------------------------------------------------------------------------------------

    ////클라우드로 부터 파일읽기

    //public static void LoadFromCloud(string key)

    //{
    //    if (!CheckLogin())

    //    {

    //        Login();

    //        return;

    //    }

    //    //내가 사용할 파일이름을 지정해줍니다. 그냥 컴퓨터상의 파일과 똑같다 생각하시면됩니다.

    //    OpenSavedGame(key, false);

    //}



    //static void OnSavedGameOpenedToRead(SavedGameRequestStatus status, ISavedGameMetadata game)

    //{

    //    if (status == SavedGameRequestStatus.Success)

    //    {

    //        // handle reading or writing of saved game.

    //        LoadGameData(game);

    //    }

    //    else

    //    {

    //        //파일열기에 실패 한경우, 오류메시지를 출력하던지 합니다.

    //    }

    //}


    ////데이터 읽기를 시도합니다.

    //static void LoadGameData(ISavedGameMetadata game)

    //{

    //    ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;

    //    savedGameClient.ReadBinaryData(game, OnSavedGameDataRead);

    //}


    //static void OnSavedGameDataRead(SavedGameRequestStatus status, byte[] data)

    //{

    //    if (status == SavedGameRequestStatus.Success)

    //    {
    //        Debug.LogError("성공");
    //        // handle processing the byte array data

    //        FileData.SetText(System.Text.Encoding.UTF8.GetString(data));
    //        //데이터 읽기에 성공했습니다.

    //        //data 배열을 복구해서 적절하게 사용하시면됩니다.
           
    //    }

    //    else

    //    {
    //        //읽기에 실패 했습니다. 오류메시지를 출력하던지 합니다.
    //    }

    //}


}
