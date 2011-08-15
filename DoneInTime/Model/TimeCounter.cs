using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Windows.Threading;
using System.Xml;
using System.IO;

namespace DoneInTime.Model
{
    public class TimeCounter
    {
        #region "Members"
        public ObservableCollection<Task> Tasks { get; set; }
        public string XmlFile { get; private set; }

        private DispatcherTimer _timer;
        #endregion

        #region "Events"
        void timer_Tick(object sender, EventArgs e)
        {
            SaveToXML();
        }
        #endregion

        #region "Methods"
        private void InitializeTimer()
        {
            _timer = new DispatcherTimer();
            _timer.Interval = new TimeSpan(0, 0, 30);
            _timer.Tick += new EventHandler(timer_Tick);
            _timer.Start();
        }

        private void LoadFromXML()
        {
            string name;
            TimeSpan ts;

            if (File.Exists(XmlFile))
            {
                using (XmlTextReader _xmlTextReader = new XmlTextReader(XmlFile))
                {
                    while (_xmlTextReader.Read())
                    {
                        if (_xmlTextReader.IsStartElement())
                        {
                            if (_xmlTextReader.Name == "Task")
                            {
                                _xmlTextReader.ReadStartElement("Task");

                                _xmlTextReader.ReadStartElement("Name");
                                name = _xmlTextReader.ReadString();
                                _xmlTextReader.ReadEndElement();

                                _xmlTextReader.ReadStartElement("TimeCount");
                                TimeSpan.TryParse(_xmlTextReader.ReadString(), out ts);
                                _xmlTextReader.ReadEndElement();

                                _xmlTextReader.ReadStartElement("IsRunning");
                                _xmlTextReader.Skip();
                                _xmlTextReader.ReadEndElement();

                                _xmlTextReader.ReadEndElement();

                                Task t = new Task(name, ts, this);
                                Tasks.Add(t);
                            }

                        }
                    }
                }
            }
        }


        public void SaveToXML()
        {
            using (XmlTextWriter _xmlTextWriter = new XmlTextWriter(XmlFile, System.Text.Encoding.UTF8))
            {
                _xmlTextWriter.Formatting = Formatting.Indented;
                _xmlTextWriter.WriteStartDocument();
                _xmlTextWriter.WriteProcessingInstruction("xml-stylesheet", "type=\"text/xsl\" href=\"TasksStyleSheet.xsl\"");
                _xmlTextWriter.WriteStartElement("Tasks");
                foreach (Task t in Tasks)
                {
                    _xmlTextWriter.WriteStartElement("Task");
                    _xmlTextWriter.WriteElementString("Name", t.Name);
                    _xmlTextWriter.WriteElementString("TimeCount", t.TimeCount.ToString());
                    _xmlTextWriter.WriteElementString("IsRunning", t.IsRunning.ToString());
                    _xmlTextWriter.WriteEndElement();
                }
                _xmlTextWriter.WriteEndElement();
            }
        }
        #endregion

        #region "Constructors"
        public TimeCounter(string xmlFile)
        {
            XmlFile = xmlFile;
            Tasks = new ObservableCollection<Task>();

            LoadFromXML();
            InitializeTimer();
        }
        #endregion
    }
}
