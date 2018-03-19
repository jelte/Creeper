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
            // Load profiles
            LoadProfiles();
        }

        private void LoadProfiles()
        {
            // Ensure the file exists before trying to open it.
            if (File.Exists(filename))
            {
                // Open a file stream
                FileStream file = File.Open(filename, FileMode.Open);
                try
                {
                    // convert the binary information to a list of profiles.
                    profiles = (List<Profile>)formatter.Deserialize(file);
                } catch (Exception e) {
                    Debug.Log(e.Message);
                }
                file.Close();
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

        public Profile ActiveProfile
        {
            get
            {
                // Create a new profile if non exists.
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
                // deactivate the active profile and activate the new profile.
                activeProfile = value;
                profiles.ForEach(delegate (Profile profile) { profile.Active = false; });
                activeProfile.Active = true;
                AddProfile(activeProfile);
            }
        }
        
        public void Update()
        {
            // Save the list of profiles to the binary save file.
            FileStream file = File.Create(filename);
            formatter.Serialize(file, profiles);
            file.Close();
        }
    }
}
