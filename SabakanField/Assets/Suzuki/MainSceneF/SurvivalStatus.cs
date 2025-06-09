using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SurvivalStatus : UIBase
{
    // óºÉ`Å[ÉÄê∂ë∂èÛãµUI

    private List<bool> _blueTeamStatus = new List<bool>();
    private List<Image> _blueTeamImage = new List<Image>();
    private List<bool> _redTeamStatus = new List<bool>();
    private List<Image> _redTeamImage = new List<Image>();
    private StringBuilder _stringBuilder = new StringBuilder();
    private Color32 _blueTeamColor = new Color32(45, 63, 255, 255);
    private Color32 _redTeamColor = new Color32(255, 45, 28, 255);
    private Color32 _deadColor = new Color32(100, 100, 100, 255);
    private const int _AI_SURVIVAERS = 5;
    public override void Execute()
    {
        CheckBlueTeamStatus();
        CheckRedTeamStatus();
    }

    public override void Initialize()
    {
        _blueTeamStatus = AIUtility.GetPlayersLife();
        _redTeamStatus = AIUtility.GetEnemysLife();
        _blueTeamImage.Clear();
        _redTeamImage.Clear();
        for (int i = 0, max = _AI_SURVIVAERS; i < max; i++)
        {
            _stringBuilder.Clear();
            _stringBuilder.Append("Blue");
            _stringBuilder.Append(i + 1);
            _blueTeamImage.Add(GameObject.Find(_stringBuilder.ToString()).GetComponent<Image>());

            _stringBuilder.Clear();
            _stringBuilder.Append("Red");
            _stringBuilder.Append(i + 1);
            _redTeamImage.Add(GameObject.Find(_stringBuilder.ToString()).GetComponent<Image>());
        }
    }

    private void CheckBlueTeamStatus()
    {
        // BlueTeamÇÃAIÇÃê∂ë∂èÛãµÇ™ï‘Ç≥ÇÍÇÈ
        _blueTeamStatus = AIUtility.GetPlayersLife();
        if (PlayerManager.GetIsPlayerDead())
            _blueTeamImage[0].color = _deadColor;
        else
            _blueTeamImage[0].color = _blueTeamColor;
        for (int i = 0, max = _blueTeamStatus.Count; i < max; i++)
        {
            // BuleTeamÇ≈Ç‚ÇÁÇÍÇƒÇ¢ÇÈèÛë‘ÇÃÇ‡ÇÃÇ™Ç¢ÇÈÇ»ÇÁÇªÇÃògÇäDêFÇ…
            if (!_blueTeamStatus[i])
                _blueTeamImage[i + 1].color = _deadColor;
            else
                _blueTeamImage[i + 1].color = _blueTeamColor;
        }
    }

    private void CheckRedTeamStatus()
    {
        // RedTeamÇÃAIÇÃê∂ë∂èÛãµÇ™ï‘Ç≥ÇÍÇÈ
        _redTeamStatus = AIUtility.GetEnemysLife();

        for (int i = 0, max = _redTeamStatus.Count; i < max; i++)
        {
            // RedTeamÇ≈Ç‚ÇÁÇÍÇƒÇ¢ÇÈèÛë‘ÇÃÇ‡ÇÃÇ™Ç¢ÇÈÇ»ÇÁÇªÇÃògÇäDêFÇ…
            if (!_redTeamStatus[i])
                _redTeamImage[i].color = _deadColor;
            else
                _redTeamImage[i].color = _redTeamColor;
        }
    }
}
