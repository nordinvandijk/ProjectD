using System.Collections.Generic;
using JetBrains.Annotations;
using Models;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class DataPanelManager : MonoBehaviour
    {
        public GameObject bigDataPanel;
        public GameObject dataPanel;
        public Texture defaultImage;
        public Text metaDataObject;
        public Text bagDataObject;
        public List<Text> titleObjects;
        public List<Text> descriptionObjects;
        public List<RawImage> imageObjects;
        private ActiveDataPanel activeDataPanel;

        public void SetActiveDataPanel()
        {
            if (Input.GetKeyDown(KeyCode.I)) activeDataPanel = ActiveDataPanel.ExtraDataPanel;

            if (Input.GetKeyDown(KeyCode.C)) activeDataPanel = ActiveDataPanel.None;

            switch (activeDataPanel)
            {
                case ActiveDataPanel.MetadataPanel:
                    dataPanel.SetActive(true);
                    bigDataPanel.SetActive(false);
                    break;
                case ActiveDataPanel.ExtraDataPanel:
                    dataPanel.SetActive(false);
                    bigDataPanel.SetActive(true);
                    break;
                default:
                    dataPanel.SetActive(false);
                    bigDataPanel.SetActive(false);
                    break;
            }
        }

        public void SetActiveDataPanel(ActiveDataPanel setActiveDataPanel)
        {
            activeDataPanel = setActiveDataPanel;

            switch (activeDataPanel)
            {
                case ActiveDataPanel.MetadataPanel:
                    dataPanel.SetActive(true);
                    bigDataPanel.SetActive(false);
                    break;
                case ActiveDataPanel.ExtraDataPanel:
                    dataPanel.SetActive(false);
                    bigDataPanel.SetActive(true);
                    break;
                default:
                    dataPanel.SetActive(false);
                    bigDataPanel.SetActive(false);
                    break;
            }
        }

        public void SetBasicData([CanBeNull] string title, [CanBeNull] string description, [CanBeNull] string id)
        {
            if (title is not null)
                foreach (var titleObject in titleObjects)
                    titleObject.text = title;

            if (description is not null || id is not null)
                foreach (var descriptionObject in descriptionObjects)
                    descriptionObject.text = description + "\n" + id;
        }

        public void SetMetadata(MetaData metaData)
        {
            metaDataObject.text =
                $"Gebouwnummer: {metaData.gebouwnummer}\n\n" +
                $"Plaats: {metaData.Address.City}, {metaData.Address.Country}\n" +
                $"Adres: {metaData.Address.Street} {metaData.Address.HouseNumber} {metaData.Address.PostalCode}\n\n" +
                $"Aantal Bouwlagen: {metaData.aantalBouwlagen}\n" +
                $"Hoogste Bouwlaag: {metaData.hoogste_bouwlaag}\n" +
                $"Laagste Bouwlaag: {metaData.laagste_bouwlaag}\n" +
                $"Gebouwd in: {metaData.yearOfConstruction}\n" +
                $"Deviation: {metaData.deviation}\n" +
                $"CreationDate: {metaData.creationDate}\n" +
                $"Begin Geldigheid: {metaData.begingeldigheid}\n" +
                $"Type Omschrijving: {metaData.typeOmschr}\n" +
                $"Hoogte: {metaData.measuredHeight}\n" +
                $"Status Omschrijving: {metaData.statusOmschr}\n" +
                $"Avineon Status: {metaData.avineonStatus}";
        }

        public void SetBagData(BagData bagData)
        {
            bagDataObject.text =
                $"Documentnummer: {bagData.pand.documentnummer}\n" +
                $"Document Datum: {bagData.pand.documentdatum}\n" +
                $"Oorspronkelijk Bouwjaar: {bagData.pand.oorspronkelijkBouwjaar}\n" +
                $"Status: {bagData.pand.status}\n" +
                $"Domein: {bagData.pand.domein}\n\n" +
                $"Tijdstip Registratie: {bagData.pand.voorkomen.tijdstipRegistratie}\n" +
                $"Tijdstip Registratie LV: {bagData.pand.voorkomen.tijdstipRegistratieLV}\n" +
                $"Versie: {bagData.pand.voorkomen.versie}\n" +
                $"Begin Geldigheid: {bagData.pand.voorkomen.beginGeldigheid}\n";
        }

        public void SetImages([CanBeNull] Texture texture)
        {
            if (texture is null)
                foreach (var image in imageObjects)
                    image.texture = defaultImage;
            foreach (var image in imageObjects) image.texture = texture;
        }
    }
}