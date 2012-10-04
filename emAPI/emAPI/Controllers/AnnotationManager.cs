
/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * DAVID SAUNTSON 12993011                                               *
 * MSc Software Engineering - Final Year Project                         *
 *                                                                       *
 * Energy Management API & Software                                      *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * 
 * AnnotationManager.cs
 * Code source: Handwritten
 */
		

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using emAPI.ClassLibrary;
using emAPI.Interfaces;

namespace emAPI.Controllers
{
    /// <summary>
    /// Controller object for Annotation model object related objects
    /// </summary>
    internal class AnnotationManager : IAnnotationManager
    {
        private EMMediator mediator;

        internal AnnotationManager()
        {
            mediator = new EMMediator();
        }

        //* * * DATA ACCESS METHODS

        public List<Annotation> getNotesForMeter(int meterId)
        {
            List<Annotation> notes = mediator.DataManager.getNotes(meterId);
            return notes;
        }


        //* * * CREATION METHODS

        public int createAnnotation(DateTime date, string note, int meterId)
        {
            Annotation newNote = new Annotation
            {
                Date = date, Note = note
            };

            return mediator.DataManager.saveNote(newNote);
        }


        //* * * UPDATE METHODS

        public Annotation editAnnotation(int noteId, string AnnotationJSON)
        {
            Annotation updatedNote = EMConverter.fromJSONtoA<Annotation>(AnnotationJSON);
            updatedNote = mediator.DataManager.editNote(noteId, updatedNote);
            return updatedNote;
        }
    }
}
