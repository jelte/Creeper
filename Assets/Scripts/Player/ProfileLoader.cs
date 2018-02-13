using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace ProjectFTP.Player
{
    class ProfileLoader
    {
        private static BinaryFormatter formatter = new BinaryFormatter();
        private string filename;
        private Profile activeProfile;
        private List<Profile> profiles = new List<Profile>();
        
        public ProfileLoader(string filename)
        {
            this.filename = filename;
            LoadProfiles();
        }

        private void LoadProfiles()
        {
            if (File.Exists(filename))
            {
                FileStream file = File.Open(filename, FileMode.Open);
                profiles = (List<Profile>)formatter.Deserialize(file);
                file.Close();
            }
        }

        public Profile ActiveProfile
        {
            get
            {
                if (activeProfile == null)
                {
                    activeProfile = profiles.Find(delegate (Profile profile) { return profile.Active; });
                    if (activeProfile == null)
                    {
                        ActiveProfile = new Profile();
                    }
                }
                return activeProfile;
            }
            set
            {
                activeProfile = value;
                profiles.ForEach(delegate (Profile profile) { profile.Active = false; });
                activeProfile.Active = true;
                AddProfile(activeProfile);
            }
        }

        private void AddProfile(Profile profile)
        {
            if (!profiles.Contains(activeProfile))
            {
                profiles.Add(profile);
                Update();
            }
        }
        
        public void Update()
        {
            FileStream file = File.Create(filename);
            formatter.Serialize(file, profiles);
            file.Close();
        }
    }
}
