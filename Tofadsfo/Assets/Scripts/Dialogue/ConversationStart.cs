using DialogueEditor;
using MyBox;
using System.Collections;
using System.Collections.Generic;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

[RequireComponent(typeof(NPCConversation))]
public class ConversationStart : MonoBehaviour
{
    [SerializeField] bool _useEvent;
    [SerializeField] protected NPCConversation _conversation;
    [SerializeField] protected ConversationManager _convoMan;
    [SerializeField] TMP_SpriteAsset _spriteAsset;
    private bool _conversationFinished;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartConversation();
    }

    public void StartConversation()
    {
        if (_conversationFinished) return;

        if(_spriteAsset!=null) _convoMan.DialogueText.spriteAsset = _spriteAsset;
        _convoMan.OnConversationStarted += StopPlayerControls;
        _convoMan.OnConversationEnded += ConversationFinished;
        _convoMan.StartConversation(_conversation);
    }
    protected void ConversationFinished()
    {
        _conversationFinished = true;
        _convoMan.OnConversationEnded -= ConversationFinished;
    }
    private void StopPlayerControls()
    {
        _convoMan.OnConversationStarted -= StopPlayerControls;
    }
    private void OnValidate()
    {
        if ((_conversation==null))
        {
            _conversation = GetComponent<NPCConversation>();
        }
    }
}
