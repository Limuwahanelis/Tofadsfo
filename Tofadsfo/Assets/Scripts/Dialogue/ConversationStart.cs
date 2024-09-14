using DialogueEditor;
using MyBox;
using System.Collections;
using System.Collections.Generic;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(NPCConversation))]
public class ConversationStart : MonoBehaviour
{
    [SerializeField] bool _useEvent;
    [SerializeField, ConditionalField("_useEvent")] TutorialEventSO _tutorialStep;
    [SerializeField] protected NPCConversation _conversation;
    [SerializeField] protected ConversationManager _convoMan;
    [SerializeField] protected UnityEvent OnConvoEnd;
    [SerializeField] protected UnityEvent OnConvoStart;
    private bool _conversationFinished;
    private void Awake()
    {
        if(_useEvent)_tutorialStep.OnStepCompleted += StartConversation;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartConversation();
    }

    public void StartConversation()
    {
        if (_conversationFinished) return;
        _convoMan.OnConversationEnded += ConversationFinished;
        _convoMan.StartConversation(_conversation);
        if(_useEvent)_tutorialStep.OnStepCompleted -= StartConversation;
    }
    protected void ConversationFinished()
    {
        _conversationFinished = true;
        OnConvoEnd?.Invoke();
        _convoMan.OnConversationEnded -= ConversationFinished;
    }
    private void OnValidate()
    {
        if ((_conversation==null))
        {
            _conversation = GetComponent<NPCConversation>();
        }
    }
    private void OnDestroy()
    {
        if (_useEvent) _tutorialStep.OnStepCompleted -= StartConversation;
    }
}
