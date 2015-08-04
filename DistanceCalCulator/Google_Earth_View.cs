/*
Copyright 2008 Google Inc.

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

     http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using GEPlugin;
namespace DistanceCalCulator
{
    
        [ComVisibleAttribute(true)]
        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        public partial class Google_Earth_View : Form
        {
            private const string PLUGIN_URL =
             @"http://earth-api-samples.googlecode.com/svn/trunk/demos/desktop-embedded/pluginhost.html";

            private IGEPlugin m_ge = null;
            private List<PlaceMark> placeMarks;

            public Google_Earth_View()
            {
                InitializeComponent();
            }

            // called from initCallback in JavaScript
            public void JSInitSuccessCallback_(object pluginInstance)
            {
                m_ge = (IGEPlugin)pluginInstance;
                //toolPanel.Enabled = true;
                ShowPlaceMarks();             
            }



            private void ShowPlaceMarks()
            {
                if (m_ge != null)
                {
                    var icon = m_ge.createIcon("");
                    icon.setHref("http://maps.google.com/mapfiles/kml/paddle/ylw-blank.png");
                    
                    var iconStyle = m_ge.createStyle(""); //create a new style
                    iconStyle.getIconStyle().setIcon(icon); //apply the icon to the style

                    var lineStyle = m_ge.createStyle("");
                    lineStyle.getLineStyle().setWidth(2);
                    lineStyle.getLineStyle().getColor().set("ffff00ff");  // aabbggrr format

                        
                    int idx;
                    for (idx = 0;idx < placeMarks.Count - 1; ++idx)
                    {
                        
                        KmlPointCoClass p1 = m_ge.createPoint(String.Empty);
                        p1.setLatitude(placeMarks[idx].latitude);
                        p1.setLongitude(placeMarks[idx].longitude);

                        KmlPointCoClass p2 = m_ge.createPoint(String.Empty);
                        p2.setLatitude(placeMarks[idx + 1].latitude);
                        p2.setLongitude(placeMarks[idx + 1].longitude);

                        // create a placemark
                        KmlPlacemarkCoClass lineStringPlacemark = m_ge.createPlacemark(String.Empty);
                        var lineString = m_ge.createLineString(String.Empty);
                        lineStringPlacemark.setGeometry(lineString);
                        
                        lineString.setTessellate(10);
                        lineString.setExtrude(10);
                        lineString.getCoordinates().pushLatLngAlt(p1.getLatitude(), p1.getLongitude(), 0);
                        lineString.getCoordinates().pushLatLngAlt(p2.getLatitude(), p2.getLongitude(), 0);

                        // Create a style and set width and color of line
                        lineStringPlacemark.setStyleSelector(lineStyle);
                        

                        KmlPlacemarkCoClass placemark = m_ge.createPlacemark(String.Empty);
                        // create a points
                        KmlPointCoClass point = m_ge.createPoint("");
                        point.setLatitude(p1.getLatitude());
                        point.setLongitude(p1.getLongitude());
                      
                        placemark.setName(placeMarks[idx].Location);
                        placemark.setGeometry(point);

                        placemark.setStyleSelector(iconStyle); //apply the style to the placemark

                        // add the placemark to the plugin
                        m_ge.getFeatures().appendChild(placemark);
                        m_ge.getFeatures().appendChild(lineStringPlacemark);
                    }

                    KmlPointCoClass lastPoint = m_ge.createPoint("");
                    lastPoint.setLatitude(placeMarks[idx].latitude);
                    lastPoint.setLongitude(placeMarks[idx].longitude);

                    KmlPlacemarkCoClass lastPlaceMark = m_ge.createPlacemark(String.Empty);
                    lastPlaceMark.setName(placeMarks[idx].Location);
                    lastPlaceMark.setGeometry(lastPoint);
                    lastPlaceMark.setStyleSelector(iconStyle);

                    // add the placemark to the plugin 
                    m_ge.getFeatures().appendChild(lastPlaceMark);
                    // set nav ontrols visible.
                    m_ge.getNavigationControl().setVisibility(m_ge.VISIBILITY_AUTO);
                    // set status barvisible.
                    m_ge.getOptions().setStatusBarVisibility(m_ge.VISIBILITY_AUTO);
                    m_ge.getLayerRoot().enableLayerById(m_ge.LAYER_ROADS,0);
                    m_ge.getLayerRoot().enableLayerById(m_ge.LAYER_BORDERS,1);

                    // where ge is an instance of GEPlugin.
                    var camera = m_ge.getView().copyAsCamera(m_ge.ALTITUDE_RELATIVE_TO_GROUND);

                    // set latitude and longitude
                    camera.setLatitude(placeMarks[0].latitude);
                    camera.setLongitude(placeMarks[0].longitude);
                    // Zoom IN.
                    camera.setAltitude(camera.getAltitude() / 10);
                    // for a list of all the KmlCamera members see:
                    // http://code.google.com/apis/earth/documentation/reference/interface_kml_camera.html

                    // update the view
                    m_ge.getView().setAbstractView(camera);
                }
            }

            // called from failureCallback in JavaScript
            public void JSInitFailureCallback_(string error)
            {
                MessageBox.Show("Error: " + error, "Plugin Load Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }

            private void Google_Earth_View_Load(object sender, EventArgs e)
            {
                //toolPanel.Enabled = true;
                webBrowserCtrl.Navigate(PLUGIN_URL);
                webBrowserCtrl.ObjectForScripting = this;
                
            }

            public void SetPlaceMarks(List<PlaceMark> PlaceMarks)
            {
                this.placeMarks = PlaceMarks;
            }

                      
        }
    }

