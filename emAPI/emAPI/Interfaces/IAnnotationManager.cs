
/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * DAVID SAUNTSON 12993011                                               *
 * MSc Software Engineering - Final Year Project                         *
 *                                                                       *
 * Energy Management API & Software                                      *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * 
 * IAnnotationManager.cs
 * Code source: Handwritten
 */
		

using System;
using emAPI.ClassLibrary;

namespace emAPI.Interfaces
{
    /// <summary>
    /// Interface for AnnotationManager objects
    /// </summary>
    internal interface IAnnotationManager
    {
        int createAnnotation(DateTime date, string note, int meterId);
        Annotation editAnnotation(int noteId, string AnnotationJSON);
        System.Collections.Generic.List<Annotation> getNotesForMeter(int meterId);
    }
}
