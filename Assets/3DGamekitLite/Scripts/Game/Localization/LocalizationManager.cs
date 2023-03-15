using Pixelplacement;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gamekit3D
{
    public class LocalizationManager : Singleton<LocalizationManager>
    {
        public List<OriginalPhrases> phrases = new List<OriginalPhrases> ();

        [SerializeField] protected int m_LanguageIndex;

        public string CurrentLanguage
        {
            get { return phrases[m_LanguageIndex].language; }
        }

        public string this [string key]
        {
            get { return phrases[m_LanguageIndex][key]; }
        }

        public bool SetLanguage (int index)
        {
            if (index >= phrases.Count || index < 0)
                return false;

            m_LanguageIndex = index;
            return true;
        }

        public bool SetLanguage (string language)
        {
            for (int i = 0; i < phrases.Count; i++)
            {
                if (phrases[i].language == language)
                {
                    m_LanguageIndex = i;
                    return true;
                }
            }
            return false;
        }

        public void SetLanguage (TranslatedPhrases translatedPhrases)
        {
            for (int i = 0; i < phrases.Count; i++)
            {
                if (phrases[i] == translatedPhrases)
                {
                    m_LanguageIndex = i;
                    return;
                }
            }
            phrases.Add (translatedPhrases);
            m_LanguageIndex = phrases.Count - 1;
        }
    }
}